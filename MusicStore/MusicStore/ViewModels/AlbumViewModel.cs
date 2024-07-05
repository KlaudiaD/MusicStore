using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using MusicStore.Models;
using ReactiveUI;

namespace MusicStore.ViewModels;

public class AlbumViewModel : ViewModelBase
{
    private readonly Album _album;
    public Album Album => _album;
    public event Action<AlbumViewModel>? RequestRemove;
    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }
    public string Artist => _album.Artist;

    public string Title => _album.Title;
    
    private Bitmap? _cover;
    
    private bool _canDelete;

    public AlbumViewModel(Album album, bool canDelete = false)
    {
        _album = album;
        CanDelete = canDelete;
        RemoveCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            if (CanDelete)
            {
                await _album.DeleteAsync();
                RequestRemove?.Invoke(this);
            }
        });
    }
    
    public bool CanDelete
    {
        get => _canDelete;
        set => this.RaiseAndSetIfChanged(ref _canDelete, value);
    }
    public async void DeleteAlbumFromDisk()
    {
        await _album.DeleteAsync(); // You will need to implement this method in your Album model
    }
    
    
    public Bitmap? Cover
    {
        get => _cover;
        private set => this.RaiseAndSetIfChanged(ref _cover, value);
    }
    
    public async Task LoadCover()
    {
        await using (var imageStream = await _album.LoadCoverBitmapAsync())
        {
            Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
        }
    }
    
    public async Task SaveToDiskAsync()
    {
        await _album.SaveAsync();

        if (Cover != null)
        {
            var bitmap = Cover;

            await Task.Run(() =>
            {
                using (var fs = _album.SaveCoverBitmapStream())
                {
                    bitmap.Save(fs);
                }
            });
        }
    }
}