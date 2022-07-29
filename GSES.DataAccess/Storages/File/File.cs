﻿using GSES.DataAccess.Consts;
using GSES.DataAccess.Entities.Bases;
using GSES.DataAccess.Storages.Bases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using F = System.IO.File;

namespace GSES.DataAccess.Storages.File
{
    public class File<T> : ITable<T> where T: BaseEntity
    {
        private const string FileName = nameof(T) + GeneralConsts.JsonExtension;
        private const string FullPath = FileConsts.FilePath + FileName;

        public async Task AddAsync(T element)
        {
            var items = await this.GetAllAsync();
            var list = items.ToList();

            if (list.Contains(element))
            {
                throw new DuplicateNameException(GeneralConsts.DuplicateErrorMessage);
            }

            list.Add(element);

            var jsonList = JsonConvert.SerializeObject(list);
            EnsureFolderExists(FileConsts.FilePath);

            await F.WriteAllTextAsync(FullPath, jsonList);
        }

        public Task DeleteAsync(T element)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync() => this.GetAsync(e => true);

        public async Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate)
        {
            if (!F.Exists(FullPath))
            {
                return new List<T>();
            }

            var serializedElements = await F.ReadAllTextAsync(FullPath);
            var elements = JsonConvert.DeserializeObject<IEnumerable<T>>(serializedElements).Where(predicate);

            return elements;
        }

        public Task UpdateAsync(T element)
        {
            throw new NotImplementedException();
        }

        public static void EnsureFolderExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
