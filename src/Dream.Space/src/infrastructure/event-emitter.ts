﻿import { autoinject } from "aurelia-framework";
import { EventAggregator } from "aurelia-event-aggregator";
import { PlaygroundViewModel} from "../common/types/playground-models";
import * as Enums from "../common/types/enums";


@autoinject
export class EventEmitter {

    constructor(private eventAggregator: EventAggregator) {

    }

    publish(eventType: "ServerError", data: ServerError);
    publish(eventType: "ValidationError", data: ValidationError);
    publish(eventType: "ChartData", data: PlaygroundViewModel);
    publish(eventType: "LayoutIndicatorMoved", data: LayoutIndicatorMoved);

    publish(eventType: EventType, data?: any) {
        this.eventAggregator.publish(eventType, data);
    }

    subscribe(eventType: "ServerError", handler: (event: ServerError) => void);
    subscribe(eventType: "ValidationError", handler: (event: ValidationError) => void);
    subscribe(eventType: "ChartData", handler: (event: PlaygroundViewModel) => void);
    subscribe(eventType: "LayoutIndicatorMoved", handler: (event: LayoutIndicatorMoved) => void);

    subscribe(eventType: EventType, handler: (event?: any) => void) {
        return this.eventAggregator.subscribe(eventType, handler);
    }
}

type EventType = "ServerError" | "ValidationError" | "ChartData" | "LayoutIndicatorMoved";

interface ValidationError {
    message: string[];
}

interface ServerError {
    message: string;
}

export interface LayoutIndicatorMoved {
    direction: Enums.Direction;
    indicatorId: number;
    layoutId: number;
}