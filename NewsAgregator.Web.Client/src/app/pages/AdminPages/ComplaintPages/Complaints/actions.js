export const loadData = () => ({
    type: 'COMPLAINT_LOADALL',
    remote: {
        url: '/api/complaint/takeall',
        type: 'get',
    }
});
