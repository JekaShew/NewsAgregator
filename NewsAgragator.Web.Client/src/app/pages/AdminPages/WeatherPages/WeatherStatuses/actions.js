export const loadData = () => ({
    type: 'WEATHERSTATUS_LOADALL',
    remote: {
        url: '/api/weatherstatus/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'WEATHERSTATUS_REMOVE',
    remote: {
        url: '/api/weatherstatus/delete/' + id,
        type: 'delete',
        apiSuccess: ['WEATHERSTATUS_REMOVE_SUCCESS', loadData()],
    }
});
