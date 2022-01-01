export const useHexColorConfig = (hex) => {
    if (hex) {
        return hex.replace('#', '');
    }

    return null;
}