using System;
using System.Collections;
using System.Collections.Generic;

class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }

    public Song(string title, string artist)
    {
        Title = title;
        Artist = artist;
    }

    public override string ToString()
    {
        return $"Title: {Title}, Artist: {Artist}";
    }
}

class CD
{
    public string DiskName { get; set; }
    private List<Song> songs;

    public CD(string diskName)
    {
        DiskName = diskName;
        songs = new List<Song>();
    }

    public void AddSong(Song song)
    {
        songs.Add(song);
    }

    public void RemoveSong(string title)
    {
        songs.RemoveAll(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public List<Song> GetSongs()
    {
        return songs;
    }

    public override string ToString()
    {
        return $"Disk: {DiskName}, Songs count: {songs.Count}";
    }
}

class MusicCatalog
{
    private Hashtable catalog;

    public MusicCatalog()
    {
        catalog = new Hashtable();
    }

    public void AddDisk(string diskName)
    {
        if (!catalog.ContainsKey(diskName))
        {
            catalog[diskName] = new CD(diskName);
            Console.WriteLine($"Disk '{diskName}' added.");
        }
    }

    public void RemoveDisk(string diskName)
    {
        if (catalog.ContainsKey(diskName))
        {
            catalog.Remove(diskName);
            Console.WriteLine($"Disk '{diskName}' removed.");
        }
    }

    public void AddSongToDisk(string diskName, Song song)
    {
        if (catalog.ContainsKey(diskName))
        {
            CD cd = (CD)catalog[diskName];
            cd.AddSong(song);
            Console.WriteLine($"Song '{song.Title}' by '{song.Artist}' added to disk '{diskName}'.");
        }
    }

    public void RemoveSongFromDisk(string diskName, string songTitle)
    {
        if (catalog.ContainsKey(diskName))
        {
            CD cd = (CD)catalog[diskName];
            cd.RemoveSong(songTitle);
            Console.WriteLine($"Song '{songTitle}' removed from disk '{diskName}'.");
        }
    }

    public void ViewAllCatalog()
    {
        Console.WriteLine("Full catalog:");
        foreach (DictionaryEntry entry in catalog)
        {
            CD cd = (CD)entry.Value;
            Console.WriteLine(cd);
            foreach (Song song in cd.GetSongs())
            {
                Console.WriteLine("  " + song);
            }
        }
    }

    public void ViewDisk(string diskName)
    {
        if (catalog.ContainsKey(diskName))
        {
            CD cd = (CD)catalog[diskName];
            Console.WriteLine(cd);
            foreach (Song song in cd.GetSongs())
            {
                Console.WriteLine("  " + song);
            }
        }
    }

    public void SearchByArtist(string artist)
    {
        Console.WriteLine($"Searching for artist: {artist}");
        bool found = false;
        foreach (DictionaryEntry entry in catalog)
        {
            CD cd = (CD)entry.Value;
            foreach (Song song in cd.GetSongs())
            {
                if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Found in disk '{cd.DiskName}': {song}");
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("No songs found for this artist.");
        }
    }
}

class Program
{
    static void Main()
    {
        MusicCatalog catalog = new MusicCatalog();

        catalog.AddDisk("Nirvana Collection");
        catalog.AddSongToDisk("Nirvana Collection", new Song("Come As You Are", "Nirvana"));
        catalog.AddSongToDisk("Nirvana Collection", new Song("Heart-Shaped Box", "Nirvana"));

        catalog.AddDisk("Red Hot Chili Peppers Collection");
        catalog.AddSongToDisk("Red Hot Chili Peppers Collection", new Song("Otherside", "Red Hot Chili Peppers"));
        catalog.AddSongToDisk("Red Hot Chili Peppers Collection", new Song("Dani California", "Red Hot Chili Peppers"));

        catalog.AddDisk("Metallica Collection");
        catalog.AddSongToDisk("Metallica Collection", new Song("Enter Sandman", "Metallica"));
        catalog.AddSongToDisk("Metallica Collection", new Song("Nothing Else Matters", "Metallica"));

        catalog.AddDisk("ACDC Collection");
        catalog.AddSongToDisk("ACDC Collection", new Song("Highway to Hell", "AC/DC"));
        catalog.AddSongToDisk("ACDC Collection", new Song("Back in Black", "AC/DC"));

        Console.WriteLine();
        catalog.ViewAllCatalog();
        Console.WriteLine();

        catalog.SearchByArtist("Nirvana");
        Console.WriteLine();
        catalog.SearchByArtist("Metallica");
    }
}
