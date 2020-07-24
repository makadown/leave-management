﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    /// <summary>
    /// Interface base de repositorios
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T> where T: class
    {
        /// <summary>
        /// Método para obtener todos los registros
        /// </summary>
        /// <returns></returns>
        ICollection<T> FindAll();
        /// <summary>
        /// Para encontrar un registro especifico por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindById(int id);
        /// <summary>
        /// Método para crear registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Create(T entity);
        /// <summary>
        /// Método para actualizar registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);
        /// <summary>
        /// Método para eliminar registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(T entity);
        /// <summary>
        /// Método para aplicar cualquier método CRUD aplicado al registro
        /// </summary>
        /// <returns></returns>
        bool Save();
    }
}