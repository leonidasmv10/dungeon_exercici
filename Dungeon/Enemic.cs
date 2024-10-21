using System;

namespace Dungeon
{
	public class Enemic
	{
		public string Nom { get; private set; }
		public int Vida { get; private set; }
		public int Atac { get; private set; }

		public static int n_instance;

		public Enemic(string nom, int vida, int atac)
		{
			Nom = nom;
			Vida = vida;
			Atac = atac;
		}

		public Enemic()
		{
			Random rand = new Random();
			Nom = "Dungeon_" + Enemic.n_instance++.ToString();
			Vida = rand.Next(100, 251);
			Atac = rand.Next(10, 31);
		}

		public int Atacar()
		{
			return Atac;
		}

		public void RebreDany(int dany)
		{
			Vida -= dany;
			if (Vida < 0) Vida = 0;
		}

		public bool EstàViu()
		{
			return Vida > 0;
		}
	}
}

