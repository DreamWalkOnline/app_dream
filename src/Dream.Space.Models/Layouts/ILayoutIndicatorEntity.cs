﻿namespace Dream.Space.Models.Layouts
{
    public interface ILayoutIndicatorEntity
    {
        int Id { get; set; }
        int PlotId { get; set; }
        int OrderId { get; set; }
        int IndicatorId { get; set; }
        string Name { get; set; }
        string LineColor { get; set; }
    }
}