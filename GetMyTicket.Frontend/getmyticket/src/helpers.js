export function formatDate(date) {
    return new Intl.DateTimeFormat("en-GB", {
        weekday: "long",
        day: "numeric",
        month: "long",
        year: 'numeric',
        hour: "2-digit",
        minute: "2-digit",
        hourCycle: "h24"
    }).format(new Date(date));
    //Monday 19 May 2025 at 13:40
}
