using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReduceEquations {
    class Matrix {
        public Fraction[,] Value;
        int col, row;

        #region プロパティ
        /// <summary>
        /// 行数（１～）
        /// </summary>
        public int Column { get { return col; } }
        /// <summary>
        /// 列数（１～）
        /// </summary>
        public int Row { get { return row; } }
        #endregion

        #region メソッド
        /// <summary>
        /// 逆行列を作る
        /// </summary>
        /// <returns>対する逆行列</returns>
        public Matrix MakeInverse() { return new Matrix(); }

        public int GetRank() { return 0; }

        #endregion

        #region 演算子
        public static Matrix operator +(Matrix obj1, Matrix obj2) { return new Matrix(); }
        public static Matrix operator -(Matrix obj1, Matrix obj2) { return new Matrix(); }
        public static Matrix operator -(Matrix obj) { return new Matrix(); }
        public static Matrix operator *(Matrix obj1, Matrix obj2) { return new Matrix(); }
        public static Matrix operator *(Fraction obj1, Matrix obj2) { return new Matrix(); }
        public static Matrix operator *(Matrix obj1, Fraction obj2) { return new Matrix(); }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// 2*2単位行列を生成
        /// </summary>
        public Matrix() { }

        /// <summary>
        /// 任意の大きさの行列を生成。要素は全て0
        /// </summary>
        /// <param name="col">行数</param>
        /// <param name="row">列数</param>
        public Matrix(int col, int row) { }

        /// <summary>
        /// 任意の行列を生成
        /// </summary>
        /// <param name="col">行数</param>
        /// <param name="row">列数</param>
        /// <param name="value">要素（a[0][0], a[1][0], ..., a[col][row]の順）</param>
        public Matrix(int col, int row, int[] value) { }
        #endregion
    }
}
