using System;
using AutoMapper;
using BookStoreWebApi.Application.BookOperations.Queries.GetBookDetail;
using BookStoreWebApi.DBOperations;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenNonExistBookIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = 999;

            FluentActions.
                Invoking(() => query.Handle()).
                Should().Throw<InvalidOperationException>().
                And.Message.
                Should().Be("Kitap Bulunamadı!");
        }

        [Fact]
        public void WherValidBookIdIsGiven_Book_ShouldNotBeReturnError()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = 2;

            FluentActions.
                Invoking(() => query.Handle()).
                Should().
                NotThrow();
        }
    }
}