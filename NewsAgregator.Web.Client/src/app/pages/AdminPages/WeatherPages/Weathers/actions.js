export const loadData = () => ({
    type: 'WEATHER_LOADALL',
    remote: {
        url: '/api/weather/takeall',
        type: 'get',
    }
});
