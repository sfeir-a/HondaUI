export function formatFrequency(minutes: number): string {
    if (minutes >= 60 * 24 * 30) {
        const value = minutes / (60 * 24 * 30);
        return `${value.toFixed(1)} months`;
    } else if (minutes >= 60 * 24) {
        const value = minutes / (60 * 24);
        return `${value.toFixed(1)} days`;
    } else if (minutes >= 60) {
        const value = minutes / 60;
        return `${value.toFixed(1)} hours`;
    } else {
        return `${minutes} minutes`;
    }
}