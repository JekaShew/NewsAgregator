export const loadData = () => ({
    type: 'WEATHER_LOADALL',
    remote: {
        url: '/api/weather/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'WEATHER_REMOVE',
    remote: {
        url: '/api/weather/delete/' + id,
        type: 'delete',
        apiSuccess: ['WEATHER_REMOVE_SUCCESS', loadData()],
    }
});
