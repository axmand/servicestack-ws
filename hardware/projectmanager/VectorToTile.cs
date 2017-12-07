using Engine.GIS.File;
using Engine.GIS.Grid;

namespace hardware.projectmanager
{

    public class VectorToTile
    {
        //瓦片输出路径
        static string _outDir = System.IO.Directory.GetCurrentDirectory() + @"\Data\Tile\";
        //测试用的读取shp目录
        static string _dir = @"C:\Users\81596\Desktop\总工办测试数据";
        //搜索的文件
        //1.点文件
        static string _ptName = "point.shp";
        //2.线文件
        static string _lineName = "line.shp";
        //3.面文件
        static string _polygonName = "polygon.shp";
        //4起始格网
        static int _startLevel = 15;
        //5终止格网
        static int _endLevel = 18;

        public static void Build()
        {
            //1.搜索点
            IShpReader pointReader = new ShpReader(_dir + @"\" + _lineName);
            WebMercatorGrid pointGrid = new WebMercatorGrid();
            for(int i = _startLevel; i < _endLevel; i++)
                pointGrid.Build(pointReader.Bounds, i);
            //1.1切片输出
            pointGrid.CutShape(pointReader, _outDir);
        }



    }
}
