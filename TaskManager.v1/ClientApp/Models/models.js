"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TaskRequest = exports.Tasks = exports.WeatherForecast = void 0;
var WeatherForecast = /** @class */ (function () {
    function WeatherForecast() {
    }
    return WeatherForecast;
}());
exports.WeatherForecast = WeatherForecast;
var Tasks = /** @class */ (function () {
    function Tasks() {
    }
    return Tasks;
}());
exports.Tasks = Tasks;
var TaskRequest = /** @class */ (function () {
    function TaskRequest() {
        this.id = 0;
        this.createdby = 0;
    }
    return TaskRequest;
}());
exports.TaskRequest = TaskRequest;
//model object for task
//  {
//    "id": 6,
//    "title": "title_update",
//    "description": "des_updaet",
//    "status": "status_update",
//    "priority": "prio_update",
//    "createdDate": "2024-04-20T10:11:58",
//    "modifiedDate": "2024-04-20T15:48:28.637",
//    "createdBy": 2,
//    "dueDate": "2024-04-20T10:17:01"
//  }
//# sourceMappingURL=models.js.map