using Engine.GIS.File;
using Engine.GIS.Grid;

namespace hardware.projectmanager
{

    public class VectorToTile
    {
        static bool _isRunning = false;

        public static bool IsRunning
        {
            get { return _isRunning; }
        }

        //瓦片输出路径
        static string _outDir = System.IO.Directory.GetCurrentDirectory() + @"\Data\Tile\";
        //测试用的读取shp目录
        static string _dir = @"C:\NNLandGISServiceRepo\Basemap\";
        //搜索的文件
        //1.点文件
        static string _ptName = "point";
        //2.线文件
        static string _lineName = "line";
        //3.面文件
        static string _polygonName = "polygon";
        //4起始格网
        static int _startLevel = 10;
        //5终止格网
        static int _endLevel = 20;

        public static void Build()
        {
            _isRunning = true;
            //1.搜索线
            IShpReader lineReader = new ShpReader(_dir + @"\" + _lineName+".shp");
            WebMercatorGrid lineGrid = new WebMercatorGrid();
            for (int i = _startLevel; i < _endLevel; i++)
                lineGrid.Build(lineReader.Bounds, i);
            //1.1线切片输出
            lineGrid.CutShapeOrderByGrid(lineReader, _outDir+ _lineName + @"\");

            //2.搜索面
            IShpReader polygonReader = new ShpReader(_dir + @"\" + _polygonName + ".shp");
            WebMercatorGrid polygonGrid = new WebMercatorGrid();
            for (int i = _startLevel; i < _endLevel; i++)
                polygonGrid.Build(polygonReader.Bounds, i);
            //2.1线切片输出
            polygonGrid.CutShapeOrderByGrid(polygonReader, _outDir + _polygonName + @"\");

            //3.搜索点
            IShpReader pointReader = new ShpReader(_dir + @"\" + _ptName + ".shp");
            WebMercatorGrid pointGrid = new WebMercatorGrid();
            for (int i = _startLevel; i < _endLevel; i++)
                pointGrid.Build(pointReader.Bounds, i);
            //3.1线切片输出
            pointGrid.CutShapeOrderByGrid(pointReader, _outDir + _ptName + @"\");
            _isRunning = false;
        }

    }
}
