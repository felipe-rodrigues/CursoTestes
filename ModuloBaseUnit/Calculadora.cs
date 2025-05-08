namespace ModuloBaseUnit
{
    public class Calculadora
    {

        public double Somar(double a, double b)
        {
            return a + b;
        }

        public double Diminuir(double a, double b)
        {
            return a - b;
        }

        public double Multiplicar(double a, double b)
        {
            return a * b;
        }

        public double Dividir(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Divisão nao aceita para valores informados.");
            return a / b;
        }
    }
}
