using MauiAppMinhasComprasFehFinal.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppMinhasComprasFehFinal.Views
{
    public partial class ListaProduto : ContentPage
    {
        ObservableCollection<Produto> Lista = new ObservableCollection<Produto>();
        public ListaProduto()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = Lista;
        } 

        protected async override void OnAppearing()
        {
            try
            {

                Lista.Clear();
                List<Produto> tmp = await App.Db.GetAll();

                tmp.ForEach(i => Lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");

            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new Views.NovoProduto());

            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { 

            string q = e.NewTextValue;

                lst_produtos.IsRefreshing = true;

                Lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => Lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
            finally
            {
                lst_produtos.IsRefreshing = false;
            }

        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            try { 
            double soma = Lista.Sum(i => i.Total);

            string msg = $"O total � {soma:C}";

            DisplayAlert("Total dos Produtos", msg, "OK");

            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }
        private async void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = sender as MenuItem;
                Produto p = selecionado.BindingContext as Produto;
                bool confirm = await DisplayAlert(
                    "Tem certeza?", $"Remover {p.Descricao}?", "Sim", "N�o");
                if(confirm)
                {
                    await App.Db.Delete(p.Id);
                    Lista.Remove(p);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }

        }

        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Produto p = e.SelectedItem as Produto;

                Navigation.PushAsync(new Views.EditarProduto
                {
                    BindingContext = p,

                });

            
            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }



        }

        private async void lst_produtos_Refreshing(object sender, EventArgs e)
        {
            try
            {

                Lista.Clear();
                List<Produto> tmp = await App.Db.GetAll();

                tmp.ForEach(i => Lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");

            }
            finally
            {
                lst_produtos.IsRefreshing = false;
            }
        }

        private async void txt_tipo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                string q = e.NewTextValue;

                lst_produtos.IsRefreshing = true;

                Lista.Clear();

                List<Produto> tmp = await App.Db.Search(q);

                tmp.ForEach(i => Lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
            finally
            {
                lst_produtos.IsRefreshing = false;
            }
        }
    }
    }