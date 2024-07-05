using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using MusicStore.Models;
using ReactiveUI;
using System.Reactive.Concurrency;

namespace MusicStore.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand BuyMusicCommand { get; }
        public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }
        public ObservableCollection<AlbumViewModel> Albums { get; } = new();

        public MainWindowViewModel()
        {
            ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();
            
            RxApp.MainThreadScheduler.Schedule(LoadAlbums);

            BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new MusicStoreViewModel();
                
                var resultViewModel = await ShowDialog.Handle(store);
                if (resultViewModel != null)
                {
                    var album = resultViewModel.Album;
                    var albumViewModel = new AlbumViewModel(album, canDelete: true);
                    Albums.Add(albumViewModel);
                    albumViewModel.RequestRemove += RemoveAlbum;
                    
                    await albumViewModel.LoadCover();
                    await albumViewModel.SaveToDiskAsync();
                }
            });
        }
        
        private async void LoadAlbums()
        {
            var albums = (await Album.LoadCachedAsync()).Select(x => new AlbumViewModel(x, true));
            foreach (var albumViewModel in albums)
            {
                Albums.Add(albumViewModel);
                albumViewModel.RequestRemove += RemoveAlbum; // Subscribe to the event
            }

            foreach (var album in Albums.ToList())
            {
                await album.LoadCover();
            }
        }
        
        private void RemoveAlbum(AlbumViewModel albumViewModel)
        {
            Albums.Remove(albumViewModel);
            albumViewModel.RequestRemove -= RemoveAlbum;
            albumViewModel.DeleteAlbumFromDisk();
        }
    }
}