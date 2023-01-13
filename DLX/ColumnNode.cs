﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ColumnNode : DataNode
{
    private int size;
    private string name;
    public ColumnNode(string name) : base()
    {
        size = 0;
        this.name = name;
        Column = this;
    }
    public void Cover()
    {
        HorizontalDisconnection();
        for (DataNode i = Down; i != this; i = i.Down)
        {
            for (DataNode j = i.Right; j != i; j = j.Right)
            {
                j.VerticalDisconnection();
                j.Column.size--;
            }
        }
    }
    public void Uncover()
    {
        for (DataNode i = Up; i != this; i = i.Up)
        {
            for (DataNode j = i.Left; j != i; j = j.Left)
            {
                j.VerticalReconnection();
                j.Column.size++;
            }
        }
        HorizontalReconnection();
    }
    public int Size
    {
        get { return size; }
        set { size = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}