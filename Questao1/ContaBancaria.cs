using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        public int Numero { get; private set; }
        public string Titular { get; set; }
        private double _saldo;

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
            _saldo = 0.0;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial) : this(numero, titular)
        {
            Deposito(depositoInicial);
        }

        public double Saldo
        {
            get { return _saldo; }
        }

        public void Deposito(double quantia)
        {
            _saldo += quantia;
        }

        public void Saque(double quantia)
        {
            _saldo -= quantia + 3.50; // 3.50 is the fee for withdrawal
        }

        public override string ToString()
        {
            return "Conta " + Numero
                + ", Titular: " + Titular
                + ", Saldo: $ " + Saldo.ToString("F2", CultureInfo.InvariantCulture);
        }
    }

}

