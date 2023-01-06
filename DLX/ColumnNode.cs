using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ColumnNode : DataNode
{
    private int size;
    private char name;
    public ColumnNode(char name) : base()
    {
        this.size = 0;
        this.name = name;
        base.Column = this;
    }
    public void cover()
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
    public void uncover()
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
    public char Name
    {
        get { return name; }
        set { name = value; }
    }
}
