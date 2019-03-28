
namespace Xtaieer.Toolbox.Save
{
    /// <summary>
    /// 代表存档要保存到的目标，比如一个磁盘文件等
    /// </summary>
    public interface IStorageTarget
    {
        /// <summary>
        /// 从存档中读取数据
        /// </summary>
        /// <returns>读取到数据</returns>
        byte[] Read();

        /// <summary>
        /// 把数据写入到存档
        /// </summary>
        /// <param name="bytes">要写入的数据</param>
        void Write(byte[] bytes);

        /// <summary>
        /// 删除一个存档
        /// </summary>
        void Delete();
    }
}
