using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using TPoint;

namespace _storage
{
    public unsafe class MyStorage
    {
        private shape[] objs;
        private int size;
        public MyStorage(int s)
        {
            size = s;
            objs = new shape[size];
        }
        public void Add(shape obj)
        {
            shape[] objs1;
            objs1 = new shape[++size];
            for (int i = 0; i < size - 1; i++)
            {
                objs1[i] = objs[i];
            }
            objs1[size - 1] = obj;
            objs = new shape[size];
            for (int i = 0; i < size; i++)
            {
                objs[i] = objs1[i];
            }
        }
        public void SetObject(int index, shape obj)
        {
            if (index < size)
            {
                objs[index] = obj;
            }
        }
        public shape GetObject(int index)
        {

            return objs[index];

        }

        public int CheckObject(shape obj)
        {
            for (int i = 0; i < size; i++)
            {
                if (objs[i] == obj)
                {
                    return i;
                }
            }
            return -1;
        }
        public void Delete(int index)
        {
            if (index < size)
            {
                shape[] objs1;
                int i;
                objs1 = new shape[--size];
                for (i = 0; i < index; i++)
                {
                    objs1[i] = objs[i];
                }
                for (int j = index + 1; j < size + 1; j++, i++)
                {
                    objs1[i] = objs[j];
                }

                objs = new shape[size];
                for (int l = 0; l < size; l++)
                {
                    objs[l] = objs1[l];
                }
            }
        }
        public int getCount()
        {
            return size;
        }
        ~MyStorage()
        {
        }
    };
}

