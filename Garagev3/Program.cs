using Garagev3.Interfaces;
using Garagev3.Services;
using Garagev3.UI;

IHandler handler = new GarageHandler();
var actions = new GarageMenuActions(handler);
IUI ui = new ConsoleUI(handler, actions);
ui.Run();
