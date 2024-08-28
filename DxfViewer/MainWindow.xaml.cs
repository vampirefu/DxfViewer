using AnyCAD.Foundation;
using AnyCAD.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static AnyCAD.Exchange.CADReader;

namespace DxfViewer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        // 定义 View 控件
        private RenderControl renderView;

        public MainWindow()
        {
            InitializeComponent();


            // 初始化
            AnyCAD.Foundation.GlobalInstance.Initialize();

            // 初始化 AnyCAD 的 3D 渲染窗口
            renderView = new RenderControl();


            // 添加到主窗口
            gd_Main.Children.Add(renderView);

            string dxfPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "矩型明渠-干渠纵断面.dxf");
            // 读取并显示 DXF 文件
            LoadDXFFile(dxfPath); // 替换为您自己的 DXF 文件路径
        }

        private void LoadDXFFile(string filePath)
        {
            var shape = ShapeIO.Open(filePath);
            if (shape == null)
                return;

            // 创建一个数据交换器用于读取 DXF 文件
            var dxfReader = new AnyCAD.Exchange.CADReader();

            OnLoadShapeHandler onLoad = (node, shaper, trf, color) => { };
            // 使用 DXF 读取器读取文件并获取文件内容
            if (dxfReader.Open(filePath, onLoad))
            {
                // 调整视图以适应读取的模型
                renderView.ZoomAll();
            }
            else
            {
                MessageBox.Show("Failed to load DXF file.");
            }
        }
    }
}
