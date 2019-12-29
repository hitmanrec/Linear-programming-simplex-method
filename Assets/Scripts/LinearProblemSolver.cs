using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class LinearProblemSolver
{/*
    public static LinearProgrammingProblem createProblem(float[] indecis)
    {
        var coeff = new DoubleVector(indecis.Length);
        for(int i = 0, l = indecis.Length; i < l; i++)
        {
            coeff[i] = indecis[i];
        }
        return new LinearProgrammingProblem(coeff);
    }

    public static LinearProgrammingProblem addConstraint(ref LinearProgrammingProblem problem, restriction rest)
    {
        var coeff = new DoubleVector(rest.indexes.Length);
        for (int i = 0, l = rest.indexes.Length; i < l; i++)
        {
            coeff[i] = rest.indexes[i];
        }
        //greater or equal (>=)
        if (rest.type == 0)
        {
            var constraint = new LinearConstraint(coeff, rest.b, ConstraintType.GreaterThanOrEqualTo);
            problem.AddConstraint(constraint);
        }
        else
        {
            var constraint = new LinearConstraint(coeff, rest.b, ConstraintType.EqualTo);
            problem.AddConstraint(constraint);
        }
        return problem;
    }

    public static LinearProgrammingProblem addUnarConstraint(ref LinearProgrammingProblem problem, int index, int varCount)
    {
        var coeff = new DoubleVector(varCount, 0.0);
        coeff[index] = 1.0;
        var constraint = new LinearConstraint(coeff, 0, ConstraintType.GreaterThanOrEqualTo);
        problem.AddConstraint(constraint);
        return problem;
    }

    public static DualSimplexSolver solveProblem(ref LinearProgrammingProblem problem, bool toMax)
    {
        var solverParams = new DualSimplexSolverParams
        {
            // Use steepest edge pivoting
            Costing = DualSimplexCosting.SteepestEdge,

            // Do not perform more than 1000 pivots
            MaxPivotCount = 1000,

            // Minimize, rather than maximize, the objective function
            Minimize = toMax ? false : true
        };

        var solver = new DualSimplexSolver();
        solver.Solve(problem, solverParams);
        
        return solver;
   }*/
}
