using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje3
{
    class Program
    {

        class Istasyon
        {
            public string durak; //Istasyonun adi
            public int bosYer; //Istasyondaki bos yer sayisi
            public int tandemSayi; //Istasyondaki tandem bisiklet sayisi
            public int normalSayi; //Istasyondaki normal bisiklet sayisi

            public Istasyon(string durak, int bosYer, int tandemSayi, int normalSayi) //constructor method
            {
                this.durak = durak;
                this.bosYer = bosYer;
                this.tandemSayi = tandemSayi;
                this.normalSayi = normalSayi;
            }

        }
        class Musteri
        {
            public int ID; //musterinin ID'si
            public int saat; //musterinin bisikleti aldigi saat
            public int dakika; //musterinin bisikleti aldigi dakika

            public Musteri(Random rand) //bisikleti kiralayan kisi icin random musteri ID'si ve alinma saati 
            {
                this.ID = rand.Next(1, 21);
                this.saat = rand.Next(0, 24);
                this.dakika = rand.Next(0, 60);

            }
        }
        class Node
        {
            public Istasyon data;
            public Node rightNode;
            public Node leftNode;
            public Musteri[] musteri;

        }
        class BinaryTree
        {
            private Node root;


            // Agaca yeni dugum ekleme
            public void insert(Musteri[] newMusteri, Istasyon durak)
            {
                Node newNode = this.root;
                Node exNode = null;


                if (root == null) //Agac bossa yeni dugum kok olur
                    root = newNode;
                else
                {

                    while (true)
                    {
                        exNode = newNode;
                        if (durak.durak.CompareTo(newNode.data.durak) < 0)
                        {
                            newNode = newNode.leftNode;
                            if (newNode == null)
                            {
                                exNode.leftNode = newNode;
                                return;
                            }
                        }
                        else
                        {
                            newNode = newNode.rightNode;
                            if (newNode == null)
                            {
                                exNode.rightNode = newNode;
                                return;
                            }
                        }
                    }
                }

                //bisiklet alma
                for (int x = 0; x < newMusteri.Length; x++)
                {
                    if (durak.normalSayi != 0)
                    {
                        durak.normalSayi -= 1;
                        durak.bosYer += 1;
                    }
                    else if (durak.tandemSayi != 0)
                    {
                        durak.tandemSayi -= 1;
                        durak.bosYer += 1;

                    }
                }
            }

            public int derinlik(Node node)
            {
                if (node == null)
                {
                    return 0;
                }
                return 1 + derinlik(node.leftNode) + derinlik(node.rightNode);
            }


            public void printEdit(Node node)
            {
                if (node == null)
                {
                    return;
                }
                printEdit(node.leftNode);
                Console.WriteLine("\t\tDurak Adı\t\t|\t\tBoş Park\t\t|\t\tTandem Bisiklet\t\t|\t\tNormal Bisiklet\t\t");
                Console.WriteLine(node.data.durak, node.data.bosYer, node.data.tandemSayi, node.data.normalSayi);
                Console.WriteLine("Müsteri Sayısı: " + node.musteri.Length);


                for (int i = 0; i < node.musteri.Length; i++)
                {
                    Console.WriteLine("Müsteri ID: " + node.musteri[i].ID);
                    Console.WriteLine("Kiralama Saati: " + node.musteri[i].saat + ":" + node.musteri[i].dakika);
                }

                printEdit(node.rightNode);

            }

            public void Search(int MusteriID) //musteri ID'sini arama
            {

                void Search(Node n, int MusteriID2)
                {
                    if (n == null)
                    {
                        return;
                    }
                    Search(n.leftNode, MusteriID2); //recursive
                    for (int i = 0; i < n.musteri.Length; i++)
                    {
                        if (MusteriID2 == n.musteri[i].ID)
                        {
                            Console.WriteLine(n.data.durak + " " + n.musteri[i].saat + ":" + n.musteri[i].dakika);
                        }
                        Search(n.rightNode, MusteriID2);
                    }
                }
                Search(root, MusteriID);

            }

            public void TheHashtable(string[] duraklar)
            {
                Hashtable parkYeri = new Hashtable();
                Hashtable normalBis = new Hashtable();
                Hashtable tandemBis = new Hashtable();

                ICollection Koleksiyon2 = normalBis.Keys;

                for (int x = 0; x < duraklar.Length; x++)
                {
                    string temporary = duraklar[x];
                    string[] temp_dizi = temporary.Split(',');

                    Istasyon durak = new Istasyon(temp_dizi[0], Convert.ToInt32(temp_dizi[1]), Convert.ToInt32(temp_dizi[2]), Convert.ToInt32(temp_dizi[3]));

                    parkYeri.Add(temp_dizi[0], durak.bosYer);
                    normalBis.Add(temp_dizi[0], durak.normalSayi);
                    tandemBis.Add(temp_dizi[0], durak.tandemSayi);

                    if (durak.bosYer > 5)
                    {
                        durak.normalSayi = durak.normalSayi + 5; //5 normal bisiklet ekleniyor
                        normalBis.Remove(temp_dizi[0]);
                        normalBis.Add(temp_dizi[0], durak.normalSayi); //guncelleme yapildi
                    }
                }

                foreach (object key in Koleksiyon2)
                    Console.WriteLine(key + " Normal Bisikletlerin Sayısı: " + normalBis[key]);

            }
                        
        }
        public class Heap
        {
            private Node[] heap; //dizi
            private int maxSize; //max genislik
            private int currentSize;

            private List<Istasyon> info;

            public Heap(int max)
            {
                maxSize = max;
                currentSize = 0;
                heap= new Node[maxSize];
            }

            public Heap()
            {
                this.info = new List<Istasyon>();
            }

            public bool isEmpty() //bos my control
            {
                return currentSize == 0;
            }


            public int Counting()
            {
                return info.Count;
            }


            public void Ekleme(Istasyon durak)
            {
                info.Add(durak);

                int index1 = Counting() - 1; // sondan baslayan cocuk index
                int index2 = (index1 - 1) / 2; //ebeveyn index

                while (index1 > 0)
                {
                    if (info[index2].normalSayi >= info[index1].normalSayi)
                    {
                        break;
                    }

                    Istasyon tempIstasyon = info[index1];

                    info[index1] = info[index2]; //cocuk index artik ebeveyn index
                    info[index2] = tempIstasyon; //kayma islemi

                }

            }

            public Istasyon Silme()
            {
                int sonIndex = Counting() - 1;

                Istasyon next = info[0];

                info[0] = info[sonIndex];
                info.RemoveAt(sonIndex);

                sonIndex -= 1;

                int parent = 0; //ebeveyn index 

                bool state = true;
                int leftChild, rightChild;
                Istasyon tempIstasyon;

                while (state)
                {
                    leftChild = (parent * 2) + 1; //dizide parent cocuk bulma formulu

                    if (sonIndex < leftChild)
                        break;

                    rightChild = leftChild + 1;

                    if (sonIndex >= rightChild)
                    {
                        if (info[leftChild].normalSayi < info[rightChild].normalSayi)
                        {
                            leftChild = rightChild;
                        }
                    }

                    if (info[parent].normalSayi >= info[leftChild].normalSayi)
                        break;
                    tempIstasyon = info[parent];
                    info[parent] = info[leftChild];
                    info[leftChild] = tempIstasyon;

                    parent = leftChild;
                }
                return next;
            }

        }

        class Son
        {
            static void Main(string[] args)
            {
                String[] duraklar = { "Inciralti,28,2,10", "Sahilevleri,8,1,11", "Dogal Yasam Parki,17,1,6", "Bostanli Iskele,7,0,5", "Kopru,9,6,0", "Fahrettin Altay,5,6,3", "Pasaport Iskele,10,2,10", "Hasanaga Parki,6,1,5" };

                int count = 0; //counter
                int musCount = 0; //musteri counter
                Random rand = new Random();
                BinaryTree binarySearch = new BinaryTree();

                Istasyon[] durakList = new Istasyon[duraklar.Length];



                for (int a = 0; a < duraklar.Length; a++)
                {
                    string temp = duraklar[a];

                    string[] temporary = temp.Split(','); //dizideki elemanlari islemek icin ayirma

                    List<Musteri> musteri = new List<Musteri>();

                    musCount = rand.Next(1, 11);

                    for (int x = 0; x < musCount; x++)
                    {
                        musteri.Add(new Musteri(rand));
                    }


                    Istasyon randomIst = new Istasyon(temporary[0], Int32.Parse(temporary[1]), Int32.Parse(temporary[2]), Int32.Parse(temporary[3])); //dizideki elemanlari istasyon olarak tanitma

                    binarySearch.insert(randomIst, musteri);

                    count += 1;

                    Hashtable hashtable = BinaryTree.TheHashtable(duraklar);



                }

                Console.Write("Müşteri ID'si: ");
                int inputID = Convert.ToInt32(Console.ReadLine());

                BinaryTree.Search(inputID);



            }
        }
           
        }
    }



