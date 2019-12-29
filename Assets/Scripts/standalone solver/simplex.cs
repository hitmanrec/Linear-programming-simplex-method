using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class simplex
{
    double[] function;
    double[] fm;
    double[][] system;
    int[] sign;
    int num_v;
    int num_l;
    bool way;

    double func;
    double[][] bv;
    double[][] sv;
    double[] istr;
    double[] th;
    double alm;
    int i_lrow;
    int i_lcol;

    TMPro.TMP_Text solText;

    public simplex(int variablesNum, int restrictionsNum, double[] indecisMain,
        bool toMax, restriction[] rests, TMPro.TMP_Text solutionText)
    {
        solText = solutionText;
        num_v = variablesNum;
        num_l = restrictionsNum;
        function = indecisMain;
        way = toMax;
        fm = new double[num_l];
        sign = new int[num_l];
        system = new double[num_l][];
        for (int i = 0; i < num_l; i++)
        {
            fm[i] = rests[i].b;
            sign[i] = rests[i].type;
            system[i] = new double[num_v];
        }
        for (int i = 0; i < num_l; i++)
        {
            for (int j = 0; j < num_v; j++)
            {
                system[i][j] = rests[i].indexes[j];
            }
        }

    }

    public void init()
    {
        int i, j;
        func = 0;
        sv = new double[num_l][];
        for (i = 0; i < num_l; i++)
            sv[i] = new double[num_v * 2];
        for (i = 0; i < num_l; i++)
        {
            for (j = 0; j < num_v; j++)
                sv[i][j] = system[i][j];
            for (; j < num_v * 2; j++)
                if (i + num_v == j)
                    if (way)
                        sv[i][j] = 1;
                    else
                        sv[i][j] = -1;
                else
                    sv[i][j] = 0;
        }
        istr = new double[num_v * 2];
        bv = new double[num_l][];
        for (i = 0; i < num_l; i++)
        {
            bv[i] = new double[2];
            bv[i][0] = i + num_v;
            bv[i][1] = fm[i];
        }
        for (i = 0; i < num_v * 2; i++)
            if (i < num_v)
                istr[i] = function[i] * -1;
            else
                istr[i] = 0;
        i_lcol = 0;
        for (i = 0; i < num_v * 2 - 1; i++)
        {
            if (istr[i] < 0)
                if (Math.Abs(istr[i + 1]) > Math.Abs(istr[i]))
                    i_lcol = i + 1;
        }
        th = new double[num_l];
        for (i = 0; i < num_l; i++)
            th[i] = bv[i][1] / sv[i][i_lcol];
        i_lrow = 0;
        for (i = 0; i < num_l - 1; i++)
            if (th[i] > th[i + 1])
                i_lrow = i + 1;
        alm = sv[i_lrow][i_lcol];
        print_result_to_screen(0,solText);
    }
    public void gen_plane()
    {
        int i, j, it_num = 0;
        double A, B;
        while (!plane_is_valid() && function_is_undefined())
        {
            A = bv[i_lrow][1];
            B = istr[i_lcol];
            func -= A * B / alm;
            double[] tmp_bv = new double[num_l];
            bv[i_lrow][0] = i_lcol;
            A = bv[i_lrow][1];
            for (i = 0; i < num_l; i++)
            {
                B = sv[i][i_lcol];
                tmp_bv[i] = bv[i_lrow][1];
                if (i != i_lrow)
                    tmp_bv[i] = bv[i][1] - A * B / alm;
                else
                    tmp_bv[i] /= alm;
            }
            for (i = 0; i < num_l; i++)
                bv[i][1] = tmp_bv[i];
            double[] tmp_istr = istr;
            B = istr[i_lcol];
            for (i = 0; i < num_v * 2; i++)
            {
                A = sv[i_lrow][i];
                tmp_istr[i] = istr[i] - A * B / alm;
            }
            istr = tmp_istr;
            double[][] tmp_sv = new double[num_l][];
            for (i = 0; i < num_l; i++)
                tmp_sv[i] = new double[num_v * 2];
            for (i = 0; i < num_l; i++)
                for (j = 0; j < num_v * 2; j++)
                {
                    tmp_sv[i][j] = sv[i][j];
                    A = sv[i_lrow][j];
                    B = sv[i][i_lcol];
                    if (i == i_lrow)
                        tmp_sv[i][j] /= alm;
                    else
                        tmp_sv[i][j] = sv[i][j] - A * B / alm;
                }
            sv = tmp_sv;
            i_lcol = 0;
            for (i = 0; i < num_l; i++)
                th[i] = bv[i][1] / sv[i][i_lcol];
            i_lrow = 0;
            for (i = 0; i < num_l - 1; i++)
                if (th[i] > th[i + 1])
                    i_lrow = i + 1;
            alm = sv[i_lrow][i_lcol];
            it_num++;
            print_result_to_screen(it_num,solText);
        }

        if (!function_is_undefined())
            Debug.Log("Целевая фукнция не ограничена, данная задача решений не имеет");
        else
        {
            Debug.Log("f(x) = " + func + " ");
            for (i = 0; i < num_l; i++)
            {
                Debug.Log("x" + (bv[i][0] + 1) + " = " + bv[i][1]);
            }
        }
    }
    public bool plane_is_valid()
    {
        bool result = true;
        if (way)
            for (int i = 0; i < num_v * 2; i++)
                if (istr[i] < 0)
                {
                    result = false;
                    break;
                }
        if (!way)
            for (int i = 0; i < num_v * 2; i++)
                if (istr[i] >= 0)
                {
                    result = false;
                    break;
                }

        return result;
    }
    public bool function_is_undefined()
    {
        for (int i = 0; i < num_l; i++)
            if (th[i] < 0)
            {
                return false;
            }
        return true;
    }
    public void print_result_to_screen(int it_num, TMPro.TMP_Text textField)
    {
        int i, j;
        if (it_num == 0)
        {
            textField.text += "Задана целевая функция:\n";
            string f_x = "";
            f_x += "f(x) = ";
            for (i = 0; i < num_v; i++)
            {
                if (i == 0)
                    f_x += function[i] + "x" + (i + 1) + " ";
                else
                {
                    if (function[i] < 0)
                        f_x += "- " + Math.Abs(function[i]) + "x" + (i + 1) + " ";
                    if (function[i] > 0)
                        f_x += "+ " + function[i] + "x" + (i + 1) + " ";
                }
            }
            string minmax;
            if (way)
                minmax = "max";
            else
                minmax = "min";
            f_x += "=> " + minmax + "\n";
            textField.text += f_x;
            textField.text += "И система ограничений:\n";
            string math_sys = "", math_sign = "";
            for (i = 0; i < num_l; i++)
            {
                for (j = 0; j < num_v; j++)
                {
                    if (j == 0)
                        math_sys += system[i][j] + "x" + (j + 1) + " ";
                    else if (system[i][j] == 1)
                        if (j == 0)
                            math_sys += "x" + 1 + " ";
                        else
                            math_sys += "+ x" + (j + 1) + " ";
                    else if (system[i][j] == -1)
                        if (j == 0)
                            math_sys += "-x" + 1 + " ";
                        else
                            math_sys += "- x" + (j + 1) + " ";
                    else
                    {
                        if (system[i][j] < 0)
                            math_sys += "- " + Math.Abs(system[i][j]) + "x" + (j + 1) + " ";
                        else
                            math_sys += "+ " + system[i][j] + "x" + (i + 1) + " ";
                        if (sign[i] == 0)
                            math_sign = "<=";
                        if (sign[i] == 1)
                            math_sign = "=";
                        if (sign[i] == 2)
                            math_sign = ">=";
                    }
                }

                math_sys += math_sign + " " + fm[i];
                math_sys += "\n";
            }
            string min_or_max;
            if (way)
                min_or_max = "максимум";
            else
                min_or_max = "минимум";
            textField.text += math_sys + "\n";
            textField.text += "Решим данную задачу на " + min_or_max + " методом симплексных таблиц.\nПостроим первый опорный план:\n" + "\n";
        }
        for (i = 0; i < num_l; i++)
        {

            textField.text += "x" + (bv[i][0] + 1) + "\t" + bv[i][1] + "\t";
            for (j = 0; j < num_v * 2; j++)
                textField.text += sv[i][j] + "\t";
            if (!plane_is_valid())
                textField.text += th[i];
            textField.text += "\n" + "\n";
        }
        textField.text += "f(x)\t" + func + "\t";
        for (i = 0; i < num_v * 2; i++)
            textField.text += istr[i] + "\t";
        textField.text += "\n";
        if (plane_is_valid())
        {
            if (plane_is_valid() && function_is_undefined())
                textField.text += "\nДанный план является оптимальным и не требует улучшения. Решение найдено." + "\n";

        }
        else
        {
            string ln_or_gn;
            if (way)
                ln_or_gn = "неположительные";
            else
                ln_or_gn = "положительные";
            string num_of_plane = "";
            if (it_num == 0)
                num_of_plane += "Первый опорный план";
            else
                num_of_plane += (it_num + 1) + "-й план также";
            textField.text += "\n" + num_of_plane + " не является оптимальным, поскольку\nв индексной строке присутствуют " + ln_or_gn + " элементы.\nErо необходимо улучшить.\n" + "\n";
        }
    }
}