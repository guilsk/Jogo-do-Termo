using System.Collections;

namespace Jogo_do_Termo
{
    public partial class Form1 : Form
    {
        int tlpAnalisado = 1;
        string[] palavras;
        Termo termo;

        public Form1()
        {
            termo = new Termo();
            palavras = new string[6];
            InitializeComponent();
            RegistrarEventos();
        }

        private void RegistrarEventos()
        {
            foreach (Button botao in tlp_teclado_1.Controls)
                botao.Click += DarPalpite;

            foreach (Button botao in tlp_teclado_2.Controls)
                if (botao.Text == "⌫") botao.Click += Apagar;
                else botao.Click += DarPalpite;

            foreach (Button botao in tlp_teclado_3.Controls)
                if (botao.Text == "ENTER") botao.Click += EnviarPalpite;
                else botao.Click += DarPalpite;

        }

        private void EnviarPalpite(object? sender, EventArgs e)
        {
            if (palavras[tlpAnalisado - 1].Length == 5)
            {
                AtualizarBotoesPainel();
                tlpAnalisado++;

                if (ComparaPalavra()) FinalizarJogo(true);
                if (tlpAnalisado == 7) FinalizarJogo(false);
            }
        }

        private void FinalizarJogo(bool x)
        {
            if (x) MessageBox.Show("Parabéns você acertou!");
            else MessageBox.Show("Que pena você perdeu! A palavra era " + termo.palavraSecreta);
            Close();
        }

        private bool ComparaPalavra()
        {
            if (palavras.Contains(termo.palavraSecreta.ToUpper()))
                return true;
            return false;
        }

        private void Apagar(object? sender, EventArgs e)
        {
            List<TextBox> lista = new List<TextBox>();

            foreach (TextBox tb in SelecionarLinha(tlpAnalisado).Controls)
                lista.Add(tb);

            foreach (TextBox tb in lista)
            {
                tb.Text = "";
                palavras[tlpAnalisado - 1] = "";
            }
        }

        private void DarPalpite(object? sender, EventArgs e)
        {
            Button botaoClicado = (Button)sender;

            string palpite = botaoClicado.Text;
            int coluna = 0;

            List<TextBox> lista = new List<TextBox>();


            foreach (TextBox tb in SelecionarLinha(tlpAnalisado).Controls)
                lista.Add(tb);

            lista.Reverse();

            foreach (TextBox tb in lista)
            {
                if (tb.Text == "")
                {
                    tb.Text = palpite;
                    palavras[tlpAnalisado - 1] += palpite;
                    return;
                }
                coluna++;
            }

        }

        private TableLayoutPanel SelecionarLinha(int tableIndex)
        {
            foreach (TableLayoutPanel linha in panel1.Controls)
                if (linha.TabIndex == tableIndex)
                    return linha;
            return null;
        }

        private TextBox SelecionarTextBox(int tabIndex, TableLayoutPanel linha)
        {
            foreach (TextBox t in linha.Controls)
                if (t.TabIndex == tabIndex)
                    return t;
            return null;
        }

        private void AtualizarBotoesPainel()
        {
            char[] letras = palavras[tlpAnalisado - 1].ToCharArray();

            foreach (Button botao in tlp_teclado_1.Controls)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (letras[i] == Convert.ToChar(botao.Text))
                        if (termo.palavraSecreta.ToUpper().ToCharArray().Contains(letras[i]))
                            AlteraCorTextBox(letras[i]);
                        else botao.Enabled = false;
                }


            }
            foreach (Button botao in tlp_teclado_2.Controls)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (botao.Text != "⌫")
                        if (letras[i] == Convert.ToChar(botao.Text))
                            if (termo.palavraSecreta.ToUpper().ToCharArray().Contains(letras[i]))
                                AlteraCorTextBox(letras[i]);
                            else botao.Enabled = false;
                }
            }

            foreach (Button botao in tlp_teclado_3.Controls)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (botao.Text != "ENTER")
                        if (letras[i] == Convert.ToChar(botao.Text))
                            if (termo.palavraSecreta.ToUpper().ToCharArray().Contains(letras[i]))
                                AlteraCorTextBox(letras[i]);
                            else botao.Enabled = false;
                }
            }
        }

        private void AlteraCorTextBox(char c)
        {
            string p = termo.palavraSecreta.ToUpper();
            for (int i = 0; i < 5; i++)
            {
                if (p.Contains(c))
                {
                    TableLayoutPanel linha = SelecionarLinha(tlpAnalisado);
                    TextBox tb = SelecionarTextBox(i, linha);
                    if (p[i] == c)
                    {
                        if (tb.Text == c.ToString()) tb.BackColor = Color.FromArgb(58, 163, 148);
                    }
                    else
                    {
                        if (tb.Text == c.ToString()) tb.BackColor = Color.FromArgb(211, 173, 105);
                    }
                }
            }


        }

        private void tb_1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb2_TextChanged(object sender, EventArgs e)
        {
        }

        private void tb3_TextChanged(object sender, EventArgs e)
        {
        }

        private void tb4_TextChanged(object sender, EventArgs e)
        {
        }
    }
}