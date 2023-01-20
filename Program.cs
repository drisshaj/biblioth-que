using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bibliotaque
{
    enum LivreEtat { Commandé, Réceptionné, Indexé, Disponible, Emprunté, Retard, Prolongé, Préréservé, Détérioré, Restauré, Archivé }
    class Bibliotheque
    {
        public List<Livre> Livres { get; set; }
        public List<Employe> Employes { get; set; }
        public List<Usager> Usagers { get; set; }

        public Bibliotheque()
        {
            Livres = new List<Livre>();
            Employes = new List<Employe>();
            Usagers = new List<Usager>();
        }

        public void AjouterLivre(Livre livre)
        {
            Livres.Add(livre);
        }

        public void AjouterEmploye(Employe employe)
        {
            Employes.Add(employe);
        }

        public void AjouterUsager(Usager usager)
        {
            Usagers.Add(usager);
        }

        public void EmprunterLivre(Livre livre, Usager usager)
        {
            livre.Emprunter(usager);
        }

        public void ProlongerEmprunt(Livre livre)
        {
            livre.Prolonger();
        }

        public void RestituerLivre(Livre livre)
        {
            livre.Restituer();
        }

        public void AfficherLivresDisponibles()
        {
            Console.WriteLine("Livres disponibles : ");
            foreach (var livre in Livres)
            {
                if (livre.Etat == LivreEtat.Disponible)
                {
                    Console.WriteLine(livre.Titre);
                }
            }
        }



       
    class Livre
    {
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public LivreEtat Etat { get; set; }
        public DateTime DateEmprunt { get; set; }
        public Usager Emprunteur { get; set; }

        public Livre(string titre, string auteur, string editeur)
        {
            Titre = titre;
            Auteur = auteur;
            Editeur = editeur;
            Etat = LivreEtat.Commandé;
        }

        public void Emprunter(Usager emprunteur)
        {
            if (Etat == LivreEtat.Disponible)
            {
                Emprunteur = emprunteur;
                Etat = LivreEtat.Emprunté;
                DateEmprunt = DateTime.Now;
            }
            else
            {
                Console.WriteLine("Le livre n'est pas disponible pour être emprunté.");
            }
        }

        public void Prolonger()
        {
            if (Etat == LivreEtat.Emprunté && (DateTime.Now - DateEmprunt).TotalDays <= 5)
            {
                DateEmprunt = DateEmprunt.AddDays(3);
                Etat = LivreEtat.Prolongé;
            }
            else
            {
                Console.WriteLine("Impossible de prolonger l'emprunt de ce livre.");
            }
        }

        public void Restituer()
        {
            if (Etat == LivreEtat.Emprunté || Etat == LivreEtat.Prolongé)
            {
                Emprunteur = null;
                Etat = LivreEtat.Disponible;
            }
            else
            {
                Console.WriteLine("Ce livre n'est pas actuellement emprunté.");
            }
        }
    }
        class Employe
        {
            public string Nom { get; set; }
            public string Poste { get; set; }

            public Employe(string nom, string poste)
            {
                Nom = nom;
                Poste = poste;
            }

            public void EnregistrerLivre(Livre livre)
            {
                if (Poste == "Biblio")
                {
                    livre.Etat = LivreEtat.Indexé;
                    Console.WriteLine("Livre enregistré et indexé.");
                }
                else
                {
                    Console.WriteLine("Vous n'êtes pas autorisé à enregistrer des livres.");
                }
            }

            public void RestaurerLivre(Livre livre)
            {
                if (Poste == "Technicien")
                {
                    livre.Etat = LivreEtat.Restauré;
                    Console.WriteLine("Livre restauré.");
                }
                else
                {
                    Console.WriteLine("Vous n'êtes pas autorisé à restaurer des livres.");
                }
            }
        }
        class Usager
        {
            public string Nom { get; set; }
            public string Adresse { get; set; }
            public List<Livre> LivresEmpruntes { get; set; }

            public Usager(string nom, string adresse)
            {
                Nom = nom;
                Adresse = adresse;
                LivresEmpruntes = new List<Livre>();
            }

            public void EmprunterLivre(Livre livre)
            {
                LivresEmpruntes.Add(livre);
            }

            public void ProlongerEmprunt(Livre livre)
            {
                if (LivresEmpruntes.Contains(livre))
                {
                    livre.Prolonger();
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas emprunté ce livre.");
                }
            }

            public void RestituerLivre(Livre livre)
            {
                if (LivresEmpruntes.Contains(livre))
                {
                    LivresEmpruntes.Remove(livre);
                    livre.Restituer();
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas emprunté ce livre.");
                }
            }

        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
