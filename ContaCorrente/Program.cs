using System;
using System.Collections.Generic;

public class ContaCorrente {
    public string Numero { get; set; }
    public double Saldo { get; private set; }
    public bool StatusEspecial { get; set; }
    public double Limite { get; set; }
    public List<string> HistoricoMovimentacoes { get; private set; }
    public Cliente Cliente { get; set; }

    public ContaCorrente(string numero, double saldoInicial, double limite, Cliente cliente) {
        Numero = numero;
        Saldo = saldoInicial;
        Limite = limite;
        Cliente = cliente;
        HistoricoMovimentacoes = new List<string>();
    }

    public bool Sacar(double valor) {
        if (valor <= Saldo + Limite) {
            Saldo -= valor;
            HistoricoMovimentacoes.Add($"Saque: -R$ {valor}");
            return true;
        }
        HistoricoMovimentacoes.Add($"Tentativa de saque: -R$ {valor} (Falha)");
        return false;
    }

    public void Depositar(double valor) {
        Saldo += valor;
        HistoricoMovimentacoes.Add($"Depósito: +R$ {valor}");
    }

    public double ConsultarSaldo() {
        return Saldo;
    }

    public void Transferir(ContaCorrente destino, double valor) {
        if (Sacar(valor)) {
            destino.Depositar(valor);
            HistoricoMovimentacoes.Add($"Transferência para conta {destino.Numero}: -R$ {valor}");
            destino.HistoricoMovimentacoes.Add($"Transferência recebida de conta {Numero}: +R$ {valor}");
        }
    }
}

public class Cliente {
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string CPF { get; set; }

    public Cliente(string nome, string sobrenome, string cpf) {
        Nome = nome;
        Sobrenome = sobrenome;
        CPF = cpf;
    }
}

