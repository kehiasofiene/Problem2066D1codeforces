using System;

class Program
{
    const int MOD = 1000000007;

    static long[,] binomial; // Matrice pour stocker les coefficients binomiaux

    // Calcul des coefficients binomiaux en utilisant la récurrence
    static void PrecomputeBinomial(int max)
    {
        binomial = new long[max + 1, max + 1];
        for (int i = 0; i <= max; i++)
        {
            binomial[i, 0] = 1;  // C(i, 0) = 1
            for (int j = 1; j <= i; j++)
            {
                binomial[i, j] = (binomial[i - 1, j - 1] + binomial[i - 1, j]) % MOD;
            }
        }
    }

    // Résolution du problème en optimisant la partie dynamique
    static long Solve(int n, int c, int m)
    {
        if (m < c || n * c < m) return 0;  // Cas de base: pas de solution

        // Array pour DP
        long[] dp = new long[m + 1];
        dp[c] = 1;  // Initialisation du cas de base (premier étage)

        // Remplir le DP pour les autres étages
        for (int i = 2; i <= n; i++)  // i représente l'étage
        {
            long[] dp_next = new long[m + 1];  // Nouveau tableau pour l'étage suivant

            // Mise à jour de dp_next[j] en fonction de dp
            for (int j = c; j <= m; j++)  // j représente le nombre total d'avions
            {
                long sum = 0;
                // Remplir dp_next[j] en utilisant la somme des transitions possibles
                for (int k = 0; k <= c && k <= j; k++)
                {
                    sum = (sum + binomial[c, k] * dp[j - k]) % MOD;
                }
                dp_next[j] = sum;
            }

            dp = dp_next;  // Mettre à jour dp pour l'étage suivant
        }

        return dp[m];  // Retourner le résultat final
    }

    static void Main()
    {
        int t = int.Parse(Console.ReadLine());  // Nombre de tests

        // Pré-calculer les coefficients binomiaux jusqu'à un maximum raisonnable (par exemple, 1000)
        PrecomputeBinomial(1000);  // Choisir une valeur maximale raisonnable pour les coefficients binomiaux

        // Traitement de chaque test
        while (t-- > 0)
        {
            string[] inputs = Console.ReadLine().Split();  // Lire les valeurs n, c, m
            int n = int.Parse(inputs[0]);
            int c = int.Parse(inputs[1]);
            int m = int.Parse(inputs[2]);

            Console.ReadLine();  // Lire la ligne de zéros inutiles

            // Afficher le résultat pour ce test
            Console.WriteLine(Solve(n, c, m));
        }
    }
}
