using System.Collections.Generic;
using System.Threading;
using System;

namespace Xtaieer.Toolbox.Save
{
    /// <summary>
    /// 存档管理器
    /// </summary>
    public abstract class SaveManager : IDisposable
    {
        private bool _disposed = false;
        private bool _currentTaskFinished = true;
        private Queue<SaveTask> _taskQueue = new Queue<SaveTask>();
        private SaveTask _currentTask = null;

        public void NewLoadTask(IStorageSource source, IStorageTarget target)
        {
            CheckDispos();
            _taskQueue.Enqueue(SaveTask.CreateLoadTask(source, target));
        }

        public void NewSaveTask(IStorageSource source, IStorageTarget target)
        {
            CheckDispos();
            _taskQueue.Enqueue(SaveTask.CreateSaveTask(target, source.Read()));
        }

        public void NewDeleteTask(IStorageTarget target)
        {
            CheckDispos();
            _taskQueue.Enqueue(SaveTask.CreateDeleteTask(target));
        }

        private void CheckDispos()
        {
            if(_disposed)
            {
                throw new Exception("Save Manager has Disposed");
            }
        }

        /// <summary>
        /// 需要外部调用，用于检测异步执行的存档操作是否完成
        /// </summary>
        public void Update()
        {
            if(_currentTask != null)
            {
                // 保证数据同步
                if (Volatile.Read(ref _currentTaskFinished))
                {
                    if (_currentTask.type == TaskType.LOAD && _currentTask.isSuccess)
                    {
                        _currentTask.source.Write(_currentTask.data);
                    }
                    OnFinishTask(_currentTask);
                    _currentTask = null;
                }
            }
            else if(_taskQueue.Count > 0)
            {
                _currentTask = _taskQueue.Dequeue();
                _currentTaskFinished = false;
                OnBeginTask(_currentTask);
            }
        }

        public void Dispose()
        {
            _disposed = true;
            OnDispose();
        }

        protected virtual void OnDispose() { }

        protected abstract void OnBeginTask(SaveTask task);

        protected abstract void OnFinishTask(SaveTask task);

        protected void ExecuteTask(SaveTask task)
        {
            try
            {
                task.Execute();
            }
            finally
            {
                // 保证数据同步
                Volatile.Write(ref _currentTaskFinished, true);
            }
        }
    }
}
