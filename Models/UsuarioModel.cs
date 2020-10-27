using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Encriptacao.Models
{
    public class UsuarioModel
    {
        [Key]

        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do usuario")]

        public string Nome { get; set; }


        [Required(ErrorMessage = "Informe o Email para Login")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 3,
            ErrorMessage = "Senha minima 3 Caracteres")]

        public String Senha { get; set; }

        //[NotMapped]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage ="Senha não Confere")]

        public String ConfirmeSenha { get; set; }

        [Required(ErrorMessage ="informe o Nível")]

        public String Nivel { get; set; }


    }
}