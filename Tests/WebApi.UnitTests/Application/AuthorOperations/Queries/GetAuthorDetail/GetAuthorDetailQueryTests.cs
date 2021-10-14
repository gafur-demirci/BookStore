using System;
using AutoMapper;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStoreWebApi.DBOperations;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNonExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId = 999;

            FluentActions.
                Invoking(() => query.Handle()).
                Should().
                Throw<InvalidOperationException>().
                And.Message.
                Should().
                Be("Yazar Bulunamadı!");
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Author_ShouldNotBeReturnError()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId = 2;

            FluentActions.
                Invoking(() => query.Handle()).
                Should().
                NotThrow();
        }
    }
}