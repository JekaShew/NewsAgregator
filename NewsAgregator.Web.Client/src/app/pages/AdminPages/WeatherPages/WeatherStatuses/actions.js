export const loadData = () => ({
    type: 'WEATHERSTATUS_LOADALL',
    remote: {
        url: '/api/weatherstatus/takeall',
        type: 'get',
    }
});
