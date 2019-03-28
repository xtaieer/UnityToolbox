namespace Xtaieer.Toolbox.Save
{
    /// <summary>
    /// 代表存档对应的游戏中的对象
    /// </summary>
    public interface IStorageSource
    {
        /// <summary>
        /// 把存档对象转化成字节数据
        /// </summary>
        /// <returns>将要存档的自己数据</returns>
        byte[] Read();

        /// <summary>
        /// 把存档写入存档对象
        /// </summary>
        /// <param name="bytes">将要写入的数据</param>
        void Write(byte[] bytes);
    }
}
