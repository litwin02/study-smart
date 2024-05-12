export interface CalendarEvent {
  day: number;
  hour: number;
  availability: number;

  // id is sent from the server, but it's not required when creating a new event
  id?: number;
}
