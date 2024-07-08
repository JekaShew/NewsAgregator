import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "COMPLAINTTYPE_LOADALL_SUCCESS":
            return {
                ...state,
                complaintTypes:
                {
                    loading: false,
                    value: action.data,
                }
            }

        case "COMPLAINTTYPE_LOADALL_START":
            return {
                ...state,
                complaintTypes:
                {
                    loading: true,
                    value:
                    [
                        {
                            id: {
                                value: '',
                            },
                            title: {
                                value: '',
                            },
                            description: {
                                value: '',
                            },
                        },
                    ],
                }
            }

        case "COMPLAINTTYPE_REMOVE_START":
            return {
                ...state,
                complaintTypes:
                {
                    ...state.complaintTypes,
                    loading: true,
                }
            }

        default:
            return state
    }
}