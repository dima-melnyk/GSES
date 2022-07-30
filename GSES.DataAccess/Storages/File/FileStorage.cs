﻿using GSES.DataAccess.Entities;
using GSES.DataAccess.Storages.Bases;

namespace GSES.DataAccess.Storages.File
{
    public class FileStorage : IStorage
    {
        public FileStorage()
        {
            this.subscribers = new File<Subscriber>();
        }

        private File<Subscriber> subscribers;
        public ITable<Subscriber> Subscribers 
        {
            get => subscribers;
            set => subscribers = (File<Subscriber>)value;
        }
    }
}
