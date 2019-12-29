using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class swapAndShowSolution : MonoBehaviour
{
    public GameObject screenToDisable, screenToEnable;
    public mainController mainCtrl;
    public TMP_Text solutionText;

    public void doWhatNameSays()
    {
        /*
        var problem = LinearProblemSolver.createProblem(mainCtrl.indexesMain);

        for(int i = 0; i < mainCtrl.totalRestrictions; i++)
        {
            problem = LinearProblemSolver.addConstraint(ref problem, mainCtrl.rests[i]);
            problem = LinearProblemSolver.addUnarConstraint(ref problem, i, mainCtrl.totalVariables);
        }

        var solution = LinearProblemSolver.solveProblem(ref problem, mainCtrl.toMax);
        
        solutionText.text = "";
        optimalX.text = "";
        double[] optXes = new double[solution.OptimalX.Length];
        for (int i = 0; i < solution.OptimalX.Length; i++) {
            optXes[i] = solution.OptimalX[i];
            optimalX.text += "X" + (i+1) + " = " + optXes[i].ToString() + "\n";
        }

        optimalZ.text = "";
        optimalZ.text = "Оптимальное значение: " + solution.OptimalObjectiveFunctionValue.ToString();
        */



        simplex solver = new simplex(mainCtrl.totalVariables, mainCtrl.totalRestrictions, mainCtrl.indexesMain, mainCtrl.toMax, mainCtrl.rests, solutionText);
        solver.init();
        solver.gen_plane();
        screenToDisable.SetActive(false);
        screenToEnable.SetActive(true);
    }
}
