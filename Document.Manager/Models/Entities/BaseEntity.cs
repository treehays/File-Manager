﻿namespace Document.Manager.Models.Entities;

public abstract class BaseEntity
{
    public string Id { get; set; } = Ulid.NewUlid().ToString();
}
