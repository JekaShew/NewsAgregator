export const loadData = () => ({
    type: 'NEWS_LOADALL',
    remote: {
        url: '/api/news/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'NEWS_REMOVE',
    remote: {
        url: '/api/news/delete/' + id,
        type: 'delete',
        apiSuccess: ['NEWS_REMOVE_SUCCESS', loadData()],
    }
});
