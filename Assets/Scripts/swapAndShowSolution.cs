using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class swapAndShowSolution : MonoBehaviour
{
    public GameObject screenToDisable, screenToEnable;
    public mainController mainCtrl;
    public TMP_Text solutionText;

    public void doWhatNameSays()
    {


        //legacy solver
        //simplex solver = new simplex(mainCtrl.totalVariables, mainCtrl.totalRestrictions, mainCtrl.indexesMain, mainCtrl.toMax, mainCtrl.rests, solutionText);
        //solver.init();
        //solver.gen_plane();

        double[,] table = new double[mainCtrl.totalRestrictions + 1, mainCtrl.totalVariables + 1];
        for (int i = 0; i < mainCtrl.totalRestrictions + 1; i++)
        {
            for (int j = 0; j < mainCtrl.totalVariables + 1; j++)
            {
                if (i < mainCtrl.totalRestrictions)
                {
                    if (j == 0)
                    {
                        table[i, j] = mainCtrl.rests[i].b;
                    }
                    else
                    {
                        table[i, j] = mainCtrl.rests[i].indexes[j - 1];
                    }
                }
                else
                {
                    if (j == 0)
                    {
                        table[i, j] = 0;
                    }
                    else
                    {
                        if (mainCtrl.toMax)
                        {
                            table[i, j] = -mainCtrl.indexesMain[j - 1];
                        }
                        else
                        {
                            table[i, j] = mainCtrl.indexesMain[j - 1];
                        }
                    }

                }
            }
        }

        //solutionText.text = "Начальная симплекс-таблица:\n";
        //for (int i = 0; i < table.GetLength(0); i++)
        //{
        //    for (int j = 0; j < table.GetLength(1); j++)
        //        solutionText.text += string.Format("{0,-8}", table[i, j]);
        //    solutionText.text += "\n";
        //}

        double[] result = new double[mainCtrl.totalVariables];
        double[,] result_table;
        //Simplex s = new Simplex(table);
        //result_table = s.Calculate(result);

        //double[,] table = { {25, -3,  5},
        //                    {30, -2,  5},
        //                    {10,  1,  0},
        //                    { 6,  3, -8},
        //                    { 0, -6, -5} };

        int[] signs = new int[mainCtrl.totalRestrictions];
        for(int i = 0, l = signs.GetLength(0); i < l; i++)
        {
            signs[i] = mainCtrl.rests[i].type;
        }
        Simplex S = new Simplex(table, signs);
        result_table = S.Calculate(result);

        solutionText.text = "Решенная симплекс-таблица:\n";
        for (int i = 0; i < result_table.GetLength(0); i++)
        {
            for (int j = 0; j < result_table.GetLength(1); j++)
                solutionText.text += string.Format("{0, -8}", Math.Round(result_table[i, j],2));
            solutionText.text += "\n";
        }

        solutionText.text += "Решение:";
        for (int i = 0; i < result.GetLength(0); i++)
            solutionText.text += string.Format("{0,-6}  {1,-8}", "\nX" + (i+1) + " = ", Math.Round(result[i],2));
        solutionText.text += string.Format("{0,-6} {1,-8}", "\nZ = ", Math.Round(result_table[result_table.GetLength(0)-1, 0], 2));

        screenToDisable.SetActive(false);
        screenToEnable.SetActive(true);
    }
}
