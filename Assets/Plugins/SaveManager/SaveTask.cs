namespace Xtaieer.Toolbox.Save
{
    public enum TaskType
    {
        LOAD,
        SAVE,
        DELETE
    }

    public class SaveTask
    {
        /// <summary>
        /// 存档操作的类型
        /// </summary>
        public TaskType type
        {
            get;
            private set;
        }

        /// <summary>
        /// 存档操作的内存对象
        /// </summary>
        public IStorageSource source;

        /// <summary>
        /// 存档操作的目标对象
        /// </summary>
        public IStorageTarget target
        {
            get;
            private set;
        }

        /// <summary>
        /// 中间数据，不同的操作不同的作用
        /// 加载操作时代表从存档中加载到的数据
        /// 保存操作时代表从从内存对象的读取中数据
        /// 删除操作未用到
        /// </summary>
        internal byte[] data
        {
            get;
            set;
        }

        public bool isSuccess
        {
            get;
            internal set;
        }

        private SaveTask() { }

        /// <summary>
        /// 创建一个加载操作
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static SaveTask CreateLoadTask(IStorageSource source, IStorageTarget target)
        {
            SaveTask saveTask = new SaveTask();
            saveTask.type = TaskType.LOAD;
            saveTask.source = source;
            saveTask.target = target;
            return saveTask;
        }

        /// <summary>
        /// 创建一个保存操作
        /// </summary>
        /// <param name="target"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SaveTask CreateSaveTask(IStorageTarget target, byte[] data)
        {
            SaveTask saveTask = new SaveTask();
            saveTask.type = TaskType.SAVE;
            saveTask.target = target;
            saveTask.data = data;
            return saveTask;
        }

        /// <summary>
        /// 创建一个删除操作
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static SaveTask CreateDeleteTask(IStorageTarget target)
        {
            SaveTask saveTask = new SaveTask();
            saveTask.type = TaskType.DELETE;
            saveTask.target = target;
            return saveTask;
        }

        public void Execute()
        {
            switch (type)
            {
                case TaskType.LOAD:
                    data = target.Read();
                    break;
                case TaskType.SAVE:
                    target.Write(data);
                    break;
                case TaskType.DELETE:
                    target.Delete();
                    break;
            }
        }
    }
}
