using System;
using AutoMapper;
using BookStoreWebApi.Application.GenreOperation.Queries.GetGenreDetail;
using BookStoreWebApi.DBOperations;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenNonExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = 999;

            FluentActions.
                Invoking(() => query.Handle()).
                Should().Throw<InvalidOperationException>().
                And.Message.
                Should().Be("Kitap Türü Bulunamadı!");
        }

        [Fact]
        public void WherValidGenreIdIsGiven_Book_ShouldNotBeReturnError()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = 3;

            FluentActions.
                Invoking(() => query.Handle()).
                Should().
                NotThrow();
        }
    }
}