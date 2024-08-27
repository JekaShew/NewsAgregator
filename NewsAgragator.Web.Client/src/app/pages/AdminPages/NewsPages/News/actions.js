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

export const aggregateNews = () => ({
    type: 'NEWS_AGGREGATE',
    remote: {
        url: '/api/news/aggregate',
        type: 'get',
        apiSuccess: ['NEWS_AGGREGATE', loadData()],
    }
});
