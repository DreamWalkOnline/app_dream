﻿export type LoginResponseType = "success" | "requiresVerification";
export type ArticleBlockAction = "Remove" | "MoveUp" | "MoveDown" | "Unset";
export type HeadingType = 'H1' | 'H2' | 'H3' | 'H4' | 'H5';
export type ChartUpdateModeName = "Reset" | "Insert" | "Append";
export type ChartTypeName = "Line" | "OHLC" | "Column";

export enum ChartType {
    OHLC = 0,
    Line = 1,
    Column = 2
}

export enum Direction {
    Up = 0,
    Down = 1
}

export enum ChartUpdateMode {
    Reset = 0,
    Insert = 1,
    Append = 2
}

export enum TransformFunction {
    First = 0,
    Max = 1,
    Sum = 2,
    Avg = 3,
    Min = 4
}

export enum CompareOperator {
    Greater = 0,
    GreaterOrEqual = 1,
    Equal = 2,
    Less = 3,
    LessOrEqual = 4,
    NotEqual = 5
}

export enum QuoteType {
    Close = 0,
    Open = 1,
    High = 2,
    Low = 3,
    Volume = 4
}

export enum RuleDataSource {
    Indicator = 0,
    HistoricalData = 1,
    Constant = 2
}

