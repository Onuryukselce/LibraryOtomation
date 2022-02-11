using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kutuphane_otomasyon.EntityLayer;
using ZedGraph;

namespace Kutuphane_otomasyon
{
    public partial class DepositGraph : Form
    {
        public DepositGraph()
        {
            InitializeComponent();
            this.zedGraphControl1.MouseWheel += ZedGraphControl1_MouseWheel;
        }

        private void ZedGraphControl1_MouseWheel(object sender, MouseEventArgs e)
        {
          /* const float scalePerDelta = 0.1f / 120;
            float Scale = 0;
            bool inv = false;
            Scale += e.Delta * scalePerDelta;
            zedGraphControl1.IsSynchronizeXAxes = true;
            zedGraphControl1.IsSynchronizeYAxes = true;
            label1.Text = e.Delta.ToString();
            label2.Text = Scale.ToString();
            zedGraphControl1.ZoomOutAll(zedGraphControl1.GraphPane);
            zedGraphControl1.ZoomStepFraction = Scale;
            zedGraphControl1.ZoomPane(zedGraphControl1.GraphPane, zedGraphControl1.ZoomStepFraction, new PointF(123.5f, 123.2f), true);
            zedGraphControl1.Refresh();
            inv = zedGraphControl1.GraphPane.IsZoomed;
            label3.Text = inv.ToString();
            if (inv != true)
            {
                zedGraphControl1.Invalidate();
            } */
        }


        private void draw()
        {


            Deposit deposit = new Deposit();
            DataTable depositTable = deposit.getDeposits();

            DataRow[] teslimEdilen = depositTable.Select("Status like '%teslim%'");
            DataRow[] emanette = depositTable.Select("Status like '%emanette%'");
            DataRow[] gec = depositTable.Select("Status = 'geç'");

            int teslimEdilenSayi = teslimEdilen.Count();
            int emanetteSayi = teslimEdilen.Count();
            int gecSayi = gec.Count();
            int toplamSayi = depositTable.Rows.Count;


            double[] toplamSArr = { toplamSayi };
            double[] teslimEdilenSArr = { teslimEdilenSayi };
            double[] emanetteSArr = { emanetteSayi };
            double[] gecSArr = { gecSayi };
            double[][] y = { toplamSArr, teslimEdilenSArr, emanetteSArr, gecSArr };
            string[] labels = { "Toplam Kitap = " + toplamSayi.ToString(), "Teslim Edilen Kitap = " + teslimEdilenSayi.ToString(), "Emanette Olan Kitap = " + emanetteSayi.ToString(), "Teslim Tarihi Geciken Kitaplar = " + gecSayi.ToString()};

            Color[] colors = { Color.Blue, Color.Green, Color.Yellow, Color.Red };
            GraphPane myPane = new GraphPane();
            zedGraphControl1.GraphPane = myPane;
            myPane.Fill = new Fill(Color.White, Color.LightSkyBlue, 45.0f);
            myPane.Chart.Fill.Type = FillType.None;
            myPane.Legend.Position = LegendPos.Float;
            myPane.Legend.IsHStack = false;
            myPane.BarSettings.Type = BarType.Cluster;
            for (int i = 0; i < 4; i++)
            myPane.AddBar(labels[i], null, y[i],colors[i]);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
            zedGraphControl1.Visible = true;
        }
        private void DepositGraph_Load(object sender, EventArgs e)
        {

            draw();
          /*  GraphPane gp = new GraphPane();
            zedGraphControl1.GraphPane = gp;
            zedGraphControl1.Dock = DockStyle.Fill;
            zedGraphControl1.IsEnableWheelZoom = true;
            PointPair a = new PointPair(1, 1);
            PointPair b = new PointPair(1, 15);


            //drawPieChart(gp);
          */
        }

        private void drawPieChart(GraphPane pane)
        {
            Deposit deposit = new Deposit();
            DataTable depositTable = deposit.getDeposits();
            DataRow[] teslimEdilen = depositTable.Select("Status like '%teslim%'");
            DataRow[] emanette = depositTable.Select("Status like '%emanette%'");
            DataRow[] gec = depositTable.Select("Status = 'geç'");

            int teslimEdilenSayi = teslimEdilen.Count();
            int emanetteSayi = teslimEdilen.Count();
            int gecSayi = gec.Count();

            string[] x0 = new string[] { "Emanetteki Kitap Sayısı", "Teslim Edilmiş Kitap Sayısı", "Geç Kalmış Kitap Sayısı" };
            double[] y0 = new double[] { emanetteSayi, teslimEdilenSayi, gecSayi };
            Color[] c0 = new Color[] { Color.Blue, Color.Green, Color.Red };

            for (int i = 0; i < 3; i++)
            {

               pane.AddPieSlice(y0[i], c0[i], 0, x0[i]);

            }

        }

        private GraphPane plotgraph()
        {

            Deposit deposit = new Deposit();
            DataTable depositTable = deposit.getDeposits();
            DataRow[] teslimEdilen = depositTable.Select("Status like '%teslim%'");
            DataRow[] emanette = depositTable.Select("Status like '%emanette%'");
            DataRow[] gec = depositTable.Select("Status = 'geç'");


            int teslimEdilenSayi = teslimEdilen.Count();
            int emanetteSayi = teslimEdilen.Count();
            int gecSayi = gec.Count();

            GraphPane pane = new GraphPane();
            pane.XAxis.IsVisible = true;
            pane.YAxis.IsVisible = true;
            string[] x0 = new string[] { "Emanetteki Kitap Sayısı", "Teslim Edilmiş Kitap Sayısı", "Geç Kalmış Kitap Sayısı" };
            double[] y0 = new double[] { emanetteSayi,teslimEdilenSayi, gecSayi };
            Color[] c0 = new Color[] { Color.Blue,Color.Green, Color.Red };
          //  pane.Fill = new Fill(Color.White, Color.White,Color.White, 45.0f);
            pane.Chart.Fill.Type = FillType.GradientByX;
           // pane.Legend.Position = LegendPos.Float;
           // pane.Legend.Location = new Location(0.30f, 0.05f, CoordType.PaneFraction, AlignH.Right, AlignV.Top);
           // pane.Legend.FontSpec.Size = 10f;
           // pane.Legend.IsHStack = false;
            for (int i = 0; i < 3; i++)
            {

                PieItem segment1 = pane.AddPieSlice(y0[i], c0[i], 0, x0[i]);

                segment1.LabelType = PieLabelType.Name_Value_Percent;
            }
            return pane;
        }

        private void zedGraphControl1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void zedGraphControl1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void zedGraphControl1_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            bool inv = zedGraphControl1.GraphPane.IsZoomed;
            if (inv != true)
            {
                zedGraphControl1.Invalidate();
            }
            
        }
    }
}
