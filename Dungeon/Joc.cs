using Dungeon.Personatges;
using System.Runtime.ConstrainedExecution;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dungeon
{
	public class Joc
	{
		private List<Personatge> personatges;
		private Enemic enemicActual;
		private Personatge personatgeActual;
		private Random random;

		private int n_personatges = 5;

		public Joc()
		{
			personatges = new List<Personatge>();
			random = new Random();
		}

		public void Logic()
		{
			GenerarPersonatges();
			enemicActual = GenerarEnemic();
			do
			{
				SeleccionarPersonatge();
				Combat(ref personatgeActual, ref enemicActual);
			} while (!FinalitzarCombat());

			Console.WriteLine("Tots els teus personatges han mort. Fi del joc.");
		}

		private void GenerarPersonatges()
		{
			for (int i = 0; i < n_personatges; i++)
			{
				switch (random.Next(3))
				{
					case 0:
						personatges.Add(new Guerrer("Guerrer_" + i.ToString()));
						break;
					case 1:
						personatges.Add(new Mag("Mag_" + i.ToString()));
						break;
					case 2:
						personatges.Add(new Arquer("Arquer_" + i.ToString()));
						break;
				}
			}
		}

		private Enemic GenerarEnemic()
		{
			return new Enemic();
		}

		private void MostrarMenu()
		{
			Console.WriteLine("\n---- Benvingut al Joc de Rol ----");
			Console.WriteLine("Personatges disponibles:");
			for (int i = 0; i < personatges.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {personatges[i].Nom} (Vida: {personatges[i].Vida}, Atac: {personatges[i].AtacBase})");
			}
			Console.WriteLine($"\nEnemic actual: {enemicActual.Nom} (Vida: {enemicActual.Vida}, Atac: {enemicActual.Atac})");
			Console.WriteLine("Introdueix el número del personatge que vols utilitzar:");
		}

		private void SeleccionarPersonatge()
		{
			MostrarMenu();
			int personatge_index = 0;
			do
			{
				personatge_index = int.Parse(Console.ReadLine());

			} while (personatge_index <= 0 || personatge_index > n_personatges);

			personatgeActual = personatges[personatge_index - 1];
		}

		private int SeleccionarAccio(Personatge personatge)
		{
			int accio = 0;
			do
			{
				Console.WriteLine("Accions disponibles:");
				Console.WriteLine("1. Atacar");
				if (personatge is Guerrer guerrer)
					Console.WriteLine("2. Atac Especial");
				else if (personatge is Mag mag)
					Console.WriteLine("2. Llançar Encantament");
				else if (personatge is Arquer arquer)
					Console.WriteLine("2. Atac Especial");

				Console.WriteLine("Selecciona una acció:");
				accio = int.Parse(Console.ReadLine());

			} while (accio != 1 && accio != 2);
			return accio;
		}

		private void Combat(ref Personatge personatge, ref Enemic enemic)
		{
			Console.WriteLine($"{personatge.Nom} contra {enemic.Nom}!");
			while (personatge.EstàViu() && enemic.EstàViu())
			{
				switch (SeleccionarAccio(personatge))
				{
					case 1:
						int danyPersonatge = personatge.Atacar();
						enemic.RebreDany(danyPersonatge);
						Console.WriteLine($"El {personatge.Nom} ataca el {enemic.Nom}!");
						Console.WriteLine($"Dany infligit: {danyPersonatge}");
						break;

					case 2:
						int danyEspecial = 0;
						if (personatge is Guerrer)
							danyEspecial = ((Guerrer)personatge).AtacEspecial();
						else if (personatge is Mag)
							danyEspecial = ((Mag)personatge).LlançarEncantament();
						else if (personatge is Arquer)
							danyEspecial = ((Arquer)personatge).AtacEspecial();

						enemic.RebreDany(danyEspecial);
						Console.WriteLine($"El {personatge.Nom} ataca el {enemic.Nom}!");
						Console.WriteLine($"Dany infligit: {danyEspecial}");
						break;
				}

				if (!enemic.EstàViu()) continue;

				{
					int danyRebut = enemic.Atacar();
					personatge.RebreDany(danyRebut);
					Console.WriteLine($"El {enemic.Nom} respon!");
					Console.WriteLine($"Dany rebut: {danyRebut}");
				}

				if (!personatge.EstàViu()) continue;

				Console.WriteLine($"Vida restant del {personatge.Nom}: {personatge.Vida}");
				Console.WriteLine($"Vida restant del {enemic.Nom}: {enemic.Vida}");

				int next_accio = 0;
				do
				{
					Console.WriteLine($"Què vols fer a continuació ?");
					Console.WriteLine($"1.Continuar atacant");
					Console.WriteLine($"2.Canviar de personatge");
					next_accio = int.Parse(Console.ReadLine());

				} while (next_accio != 1 && next_accio != 2);

				switch (next_accio)
				{
					case 2:
						SeleccionarPersonatge();
						break;
				}

			}
		}


		private bool FinalitzarCombat()
		{
			if (enemicActual.EstàViu())
			{
				Console.WriteLine($"El {personatgeActual.Nom} ha estat derrotat!");
				Console.WriteLine($"Selecciona un altre personatge per continuar la lluita.");
				personatges.Remove(personatgeActual);
				n_personatges--;
			}
			else
			{
				Console.WriteLine($"Felicitats! Has derrotat el {enemicActual.Nom}!");
				Console.WriteLine($"Tots els personatges guanyen 20 punts de vida.");

				for (int i = 0; i < n_personatges; i++)
				{
					personatges[i].Vida += 20;
					if (personatges[i].Vida > 100) personatges[i].Vida = 100;
				}

				enemicActual = GenerarEnemic();
				Console.WriteLine($"S'està generant un nou enemic...");
				Console.WriteLine($"Enemic: {enemicActual.Nom}(Vida: {enemicActual.Vida}, Atac: {enemicActual.Atac})");
			}

			return personatges.Count == 0;
		}
	}

}
