using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LSD_CORP
{
    class DataBase
    {
        private LsdCorpContext _context = new();
        private static DataBase instance;

        public static DataBase Instance { get => instance ??= new(); }
        public LsdCorpContext GetContext { get => _context; }

        public async Task<bool> AddMat(Material mat)
        {
            if (mat == null || _context.Materials.Contains(mat))
                return false;

            await _context.Materials.AddAsync(mat);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddFur(Furniture fur)
        {
            if (fur == null || _context.Furnitures.Contains(fur))
                return false;

            await _context.Furnitures.AddAsync(fur);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddClient(Client clnt)
        {
            if (clnt == null || _context.Clients.Contains(clnt))
                return false;

            await _context.Clients.AddAsync(clnt);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DelMat(Material mat)
        {
            if (mat == null || !_context.Materials.Contains(mat))
                return false;

            _context.Materials.Remove(mat);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DelFur(Furniture fur)
        {
            if (fur == null || !_context.Furnitures.Contains(fur))
                return false;

            _context.Furnitures.Remove(fur);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdMat(Material mat)
        {
            if (mat == null)
                return false;

            _context.Materials.Update(mat);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdFur(Furniture fur)
        {
            if (fur == null)
                return false;

            _context.Furnitures.Update(fur);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Material> SearchMatById(int id)
            => _context.Materials.FirstOrDefault(s => s.Id == id) ?? new();
        public async Task<Furniture> SearchFurById(int id)
            => _context.Furnitures.FirstOrDefault(s => s.Id == id) ?? new();

        public async Task<List<Material>> GetAllMaterials()
            => [.. _context.Materials];
        public async Task<List<Client>> GetAllClients()
            => [.. _context.Clients];
        public async Task<List<Furniture>> GetAllFurnitures()
            => [.. _context.Furnitures];

        public async Task<bool> Authorization(User user)
        {
            _context = new();
            return await _context.Users.FirstOrDefaultAsync(s => s.Login == user.Login && s.Password == user.Login) != null;
        }
        public async Task<bool> Registraition(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Login) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrEmpty(user.Login) ||
                string.IsNullOrEmpty(user.Password))
                return false;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DupeFur(Furniture fur)
        {
            if (fur == null)
                return false;

            fur.Id = 0;
            await _context.Furnitures.AddAsync(fur);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DelClient(Client cl)
        {
            if (cl == null || !_context.Clients.Contains(cl))
                return false;

            _context.Furnitures.RemoveRange(_context.Furnitures.Where(s=>s.ClientId == cl.Id));
            _context.Clients.Remove(cl);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DupeCl(Client cl)
        {
            if (cl == null)
                return false;

            cl.Id = 0;
            await _context.Clients.AddAsync(cl);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}