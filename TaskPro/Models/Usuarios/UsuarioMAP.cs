﻿using TaskPro.Data;

namespace TaskPro.Models.Usuarios
{
    public static class UsuarioMAP
    {
        public static UsuarioDTO toDTO(this Usuario obj)
        {
            return new UsuarioDTO()
            {
                Nombre = obj.Nombre,
                Email = obj.Email,
                Id = obj.Id,
            };
        }
    }
}
