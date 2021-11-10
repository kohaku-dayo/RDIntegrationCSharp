using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniEx;
using System.Diagnostics;
using Debug = UniEx.Debug;

namespace LogicTest
{

    #region AtCoder

    // AtCoder Beginner Contest 223 
    // 2021-10-17(日) 21:00～2021-10-17(日) 22:40(100分)
    // テスト C - DouKasen
    // ※解決済み
    public class LogicA : UniCycle
    {
        public override void Awake()
        {
            try
            {
                int lineLength = int.Parse(Console.ReadLine());

                string[][] input = new string[lineLength][];
                for (int i = 0; i < lineLength; i++) input[i] = Debug.Scan().Split(' ');

                Vector2[] vec =
                    (from i in input
                     let v = new Vector2(double.Parse(i[0]), double.Parse(i[1]))
                     select v).ToArray();

                double t = 0, ans = 0;
                for (int i = 0; i < lineLength; i++) t += vec[i].x / vec[i].y;
                t /= 2;

                for(int i = 0; i < lineLength; t-= Math.Min(vec[i].x / vec[i].y, t), i++)
                {
                    ans += Math.Min(vec[i].x, t * vec[i].y);
                }

                Debug.Log(ans);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                Debug.Log(ex.StackTrace);
            }
        }
    }

    #endregion

    public class LogicTaple : UniCycle
    {
        public override void Awake()
        {
        }
    }

    #region アルゴリズムとデータ構造

    public class LogicB : UniCycle
    {
        public override bool doInvoke => false;
        public override bool doLog => true;
        public override void Awake()
        {
            List<testComp> tcomps = new List<testComp>();
            tcomps.Add(new testComp("a", 0));
            tcomps.Add(new testComp("c", 2));
            tcomps.Add(new testComp("e", 0));
            tcomps.Add(new testComp("a", 0));
            tcomps.Add(new testComp("g", 6));
            tcomps.Add(new testComp("i", 0));
            tcomps.Add(new testComp("g", 1));
            tcomps.Add(new testComp("a", 0));
            tcomps.Add(new testComp("c", 10));
            Stopwatch st = new Stopwatch();

            st.Start();
            var m = Sort.BubbleSort<testComp>(tcomps.ToArray());
            st.DoLog();
            void xx() { System.Threading.Thread.Sleep(100); }
            var x = (Action)xx;

            Debug.LogAccurateProcessSpeed(x, 1);
            //foreach (var x in m) Debug.Log($"{x.str} {x.num}");
        }

    }
    class testComp : IComparable<testComp>
    {
        public string str;
        public int num;
        public testComp(string str, int num)
        {
            this.str = str;
            this.num = num;
        }

        public int CompareTo(testComp other)
        {
            return this.str.CompareTo(other.str);
        }
    }

    /// <summary>
    /// ソートクラス
    /// </summary>
    public class Sort
    {
        // バブルソート
        public static List<T> BubbleSort<T>(T[] a)
            where T : IComparable<T>
        {
            int n = a.Length;
            for (int i = 0; i < n - 1; i++)
                for (int j = n - 1; j > i; j--)
                    if (a[j].CompareTo(a[j - 1]) < 0)
                        Swap(ref a[j], ref a[j - 1]);
            return a.ToList();
        }

        // 二つの変数をスワッピング
        public static void Swap<T>(ref T a, ref T b)
        {
            T c = a; a = b; b = c;
        }
    }

    #endregion

    #region パフォーマンス改善

    public class Performance : UniCycle
    {
        public override bool doInvoke => false;
        public override void Awake()
        {
            Debug.Log(Debug.Scan());
        }

        #region リストのアロケーション軽減
        // リストに配列の要素数を予め指定しておくと、
        // 指定要素数内のAdd関数やRemove関数などを実行した際に、
        // 追加のアロケーションが発生しなくなります。
        // 指定要素数以上の配列要素を確保しようとした場合には通常通り、
        // 追加のアロケーションが発生します。
        void ListTest()
        {
            // 通常のリスト
            var list = new List<int>();
            // 初期値ありのリスト
            var list_greater = new List<int>(10); // 初期配列内のAddは追加アロケーション無し
        }
        #endregion
        #region StringBuilder
        // ・stringは読み取り専用(readonly)
        // stringオブジェクトの内容の変更は不可能。
        // よってstringの連結、置換、挿入をする際には
        // 毎回、新しいstringオブジェクトが作成されている。
        //
        // ・StringBuilderは内容を変更可能。
        // 値が変更されてもヒープアロケーションは発生しない。
        // StringBuilderは予め要素数も指定することができるため、
        // アロケーションを抑えることも可能
        // #region リストのアロケーション軽減と仕組みは同様であるため、
        // ここでは説明しません。
        void StringBuilder_Test()
        {
            // 通常の文字列連結
            var line = "";
            line += "Hello";
            line += ", ";
            line += "World";
            line += "!";

            // StringBuilderありの文字列連結でヒープアロケーションを軽減
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Hello");
            stringBuilder.Append(",");
            stringBuilder.Append("World");
            stringBuilder.Append("!");

            // StringBuilderに初期要素数を設定して更にアロケーションを軽減
            var stringBuilder2 = new StringBuilder(10);
            stringBuilder2.Append("Hello");
            stringBuilder2.Append(",");
            stringBuilder2.Append("World");
            stringBuilder2.Append("!");

            Debug.Log(line);
            Debug.Log(stringBuilder); // StringBuilderはそのまま出力可
            Debug.Log(stringBuilder2);
        }
        #endregion
    }

    #endregion

}