﻿namespace ChildObjectsEf.Domain;

public class OrderItem
{
    public int Id { get; internal set; }

    public string Name { get; internal set; }

    public int Quantity { get; internal set; }

    public Order Order { get; internal set; }

    public OrderItem(int id, string name, int quantity)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Order = new Order(default, default);
    }
}